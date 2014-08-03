namespace KS.PizzaEmpire.Unity
{	
	using System;
	using System.Collections.Generic;
	using System.Text;
	using UnityEngine;
	using LitJson;
	using Common;
	
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
		/// Provides the Singleton instance of the ServerCommunicator
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
		private string GetErrorString(ErrorCode ec)
		{
			switch(ec)
			{
				case ErrorCode.ERROR_OK:
					return string.Empty;
				case ErrorCode.CONNECTION_ERROR:
					return "There was a problem connecting to the server";
				case ErrorCode.ERROR_RETRIEVING_ACCOUNT:
					return "There was a problem communicating with the server";
				default:
					return string.Empty;
			}			
		}
		
		/// <summary>
		/// Parses the response from a ServerCommunication object.
		/// </summary>
		/// <returns>The response in JsonData form or null if error.</returns>
		/// <param name="com">The ServerCommunication whose response should be parsed</param>
		public T ParseResponse<T>(ServerCommunication com)
		{
			T response = default(T);
			
			WWW www = com.Request;			
			JsonData data = JsonMapper.ToObject(www.text);					
			int ec =(int)data["ErrorCode"];
			if (ec == (int)ErrorCode.ERROR_OK)
			{
				data = data["Item"];
				string json = JsonMapper.ToJson(data);
				Debug.Log(DateTime.Now + ":" + json);
				response = JsonMapper.ToObject<T>(json);
			}
			else
			{
				com.Error = (ErrorCode)ec;
				com.ErrorMessage = GetErrorString(com.Error);	
				
				if (com.OnError != null)
				{
					com.OnError(com);
				}						
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
				case ServerActionEnum.GetBuildableItems:
					return "buildableitem";
				case ServerActionEnum.GetExperienceLevels:
					return "experiencelevel";
				case ServerActionEnum.CreatePlayer:
					return "createplayer/" + PlayerKey;
				case ServerActionEnum.GetPlayer:
					return "gameplayer/" + PlayerKey;
				case ServerActionEnum.StartWork:
					return "startwork/" + PlayerKey;
				case ServerActionEnum.FinishWork:
					return "finishwork/" + PlayerKey;
				case ServerActionEnum.SetTutorialStage:
					return "tutorialstage/" + PlayerKey;
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
		public void Communicate(ServerActionEnum serverAction, Action<ServerCommunication> onComplete, Action<ServerCommunication> onError)
		{
			Communicate(serverAction, null, onComplete, onError);
		}		
		
		/// <summary>
		/// Communicate with the server
		/// </summary>
		/// <param name="onComplete">The function to perform when the request is received</param>
		/// <param name="serverAction">The action to perform</param>
		/// <param name="data">The data to send to the server</param>
		public void Communicate(ServerActionEnum serverAction, object data, Action<ServerCommunication> onComplete, Action<ServerCommunication> onError)
		{
			ServerCommunication com = new ServerCommunication();
			com.ServerAction = serverAction;
			com.Data = data;
			com.Request = new UnityEngine.WWW(URL(serverAction,data));
			com.OnComplete = onComplete;
			com.OnError = onError;
			communications.Add(com);
			Debug.Log(DateTime.Now + ":" + com.Request.url);
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
                        Debug.Log(DateTime.Now + ":" + com.Request.error);
						com.Error = ErrorCode.CONNECTION_ERROR;
						com.ErrorMessage = GetErrorString(ErrorCode.CONNECTION_ERROR);						
						if (com.OnError != null)
						{
							com.OnError(com);
						}
					}
					else
					{
                        Debug.Log(DateTime.Now + ":" + com.Request.text);
						if (com.OnComplete != null)
						{
							com.OnComplete(com);
						}
					}
					toRemove.Add(i);
				}
			}
			
			for(int j = toRemove.Count - 1;j >= 0;j--)
			{
				communications.RemoveAt(toRemove[j]);
			}
		}
	}
}