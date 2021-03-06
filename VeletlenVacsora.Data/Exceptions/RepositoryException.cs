﻿using System;
using System.Runtime.Serialization;

namespace VeletlenVacsora.Data.Exceptions
{
	[Serializable]
	internal class RepositoryException : ApplicationException
	{
		public RepositoryException()
		{
		}

		public RepositoryException(string message) : base(message)
		{
		}

		public RepositoryException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}