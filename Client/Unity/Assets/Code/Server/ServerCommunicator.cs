namespace KS.PizzaEmpire.Unity
{	
	using System;
	using System.Collections.Generic;
	using System.Text;
	using UnityEngine;
	using LitJson;
	
	/// <summary>
	/// Represents an item that can communicate with the server 
	/// </summary>
	public class ServerCommunicator	
	{
		private static volatile ServerCommunicator instance;
		private static object syncRoot = new object();
		
		private string serverURL;
		private StringBuilder stringBuilder;		
		private List<ServerCommunication> communications;	
		private List<int> toRemove;
					
		private ServerCommunicator() 
		{
			serverURL = "http://localhost:65023/api/";
			stringBuilder = new StringBuilder();
			PlayerKey = "kevin";
			communications = new List<ServerCommunication>();		
			toRemove = new List<int>();
		}			
		
		/// <summary>
		/// Provides the Singleton instance of the RedisCache
		/// </summary>
		public static ServerCommunicator Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new ServerCommunicator();
						}
					}
				}
				return instance;
			}
		}
		
		public string PlayerKey { get; set; }		
		
		/// <summary>
		/// Returns	an appropriate error message for server error codes
		/// </summary>
		/// <returns>The error string.</returns>
		/// <param name="ec">Ec.</param>
		private string GetErrorString(ServerErrorEnum ec)
		{
			switch(ec)
			{
				case  ServerErrorEnum.ERROR_OK:
					return string.Empty;
				case ServerErrorEnum.CONNECTION_ERROR:
					return "There was a problem connection to the server";
				case ServerErrorEnum.ERROR_RETRIEVING_ACCOUNT:
					return "There was a problem communicating with the server";
				default:
					return string.Empty;
			}			
		}
		
		/// <summary>
		/// Parses the response from a ServerCommunication object.
		/// </summary>
		/// <returns>The response in JsonData form or null if error.</returns>
		/// <param name="communication">The ServerCommunication communication whose response should be parsed</param>
		public T ParseResponse<T>(ServerCommunication communication)
		{
			T response = default(T);
			
			WWW www = communication.Request;			
			JsonData data = JsonMapper.ToObject(www.text);					
			int ec =(int)data["ErrorCode"];
			if (ec == (int)ServerErrorEnum.ERROR_OK)
			{
				data = data["Item"];
				string json = JsonMapper.ToJson(data);
				Debug.Log(json);
				response = JsonMapper.ToObject<T>(json);
			}
			else
			{
				communication.Error = (ServerErrorEnum)ec;
				communication.ErrorMessage = GetErrorString(communication.Error);							
			}			
			
			return response;
		}

		/// <summary>
		/// Returns the proper API location for the requested action
		/// </summary>
		/// <param name="serverAction">The serverAction whose API location should be returned</param>		
		private string GetAPIForServerAction(ServerActionEnum serverAction)
		{
			switch (serverAction)
			{
				case ServerActionEnum.None:
					return string.Empty;
				case ServerActionEnum.CreatePlayer:
					return "createplayer";
				case ServerActionEnum.GetPlayer:
					return "gameplayer";
				case ServerActionEnum.StartWork:
					return "startwork";
				case ServerActionEnum.FinishWork:
					return "finishWork";
				default:
					return string.Empty;
			}
		}
		
		/// <summary>
		/// Returns the proper URL to communicate with the server API		
		/// </summary>
		/// <param name="api">The api entry</param>
		/// <param name="data">the data to send to the api</param>
		private string URL(ServerActionEnum serverAction, object data)
		{
			string api = GetAPIForServerAction(serverAction);
			stringBuilder.Remove(0, stringBuilder.Length);
			stringBuilder.Append(serverURL);
			stringBuilder.Append(api);
			stringBuilder.Append("/");
			stringBuilder.Append(PlayerKey);
			if (data != null)
			{
				stringBuilder.Append("@@");
				stringBuilder.Append(data.ToString());
			}
			return stringBuilder.ToString();			
		}
		
		/// <summary>
		/// Communicate with the server
		/// </summary>
		/// <param name="fn">The function to perform when the request is received</param>
		/// <param name="action">The action to perform</param>
		public void Communicate(Action<ServerCommunication> fn, ServerActionEnum action)
		{
			Communicate(fn, action, null);
		}		
		
		/// <summary>
		/// Communicate with the server
		/// </summary>
		/// <param name="onComplete">The function to perform when the request is received</param>
		/// <param name="serverAction">The action to perform</param>
		/// <param name="data">The data to send to the server</param>
		public void Communicate(Action<ServerCommunication> onComplete, ServerActionEnum serverAction, object data)
		{
			ServerCommunication com = new ServerCommunication();
			com.ServerAction = serverAction;
			com.Data = data;
			com.Request = new UnityEngine.WWW(URL(serverAction,data));
			com.OnComplete = onComplete;
			communications.Add(com);
			Debug.Log(com.Request.url);
		}		
		
		/// <summary>
		/// Updates the server communicator. Checks for requests that have completed.
		/// </summary>
		public void Update()
		{
			toRemove.Clear();
			for (int i = 0;i < communications.Count;i++)
			{
				ServerCommunication com = communications[i];
				if (com.Request.isDone)
				{				
					if (!string.IsNullOrEmpty(com.Request.error)) 
					{
						com.Error = ServerErrorEnum.CONNECTION_ERROR;
						com.ErrorMessage = GetErrorString(ServerErrorEnum.CONNECTION_ERROR);
					}
					else
					{
						if (com.OnComplete != null)
						{
							com.OnComplete(com);
						}
					}
					toRemove.Add(i);
				}
			}
			
			for(int j = toRemove.Count - 1;j > 0;j--)
			{
				communications.RemoveAt(toRemove[j]);
			}
		}
	}
}