using System;
namespace Service.Services.Helpers.Exceptions
{
	public class EmptyException:Exception
	{
		public EmptyException(string msj) : base(msj) { }
		
	}
}

