﻿namespace KS.PizzaEmpire.Unity
{	
	using UnityEngine;	
	using System;
	using Common;
	
	/// <summary>
	/// Represents a communication with the server
	/// </summary>
	public class ServerCommunication
	{
		public ServerActionEnum ServerAction { get; set; }
		public object Data { get; set; }
		public WWW Request { get; set; }
		public Action<ServerCommunication> OnComplete { get; set; }
		public Action<ServerCommunication> OnError { get; set; }
		public object Response { get; set; }
		public ErrorCode Error { get; set; }
		public string ErrorMessage { get; set; }
	}
}