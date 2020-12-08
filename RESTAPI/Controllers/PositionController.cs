using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RESTAPI.Controllers
{
	[ApiController]
	[Route("{controllers}")]
	public class PositionController : ControllerBase
	{
		[HttpGet]
		//[Route("/position")]
		public IActionResult GetAll()
		{
			var directory = Directory.GetCurrentDirectory();
			string[] filePaths = Directory.GetFiles($"{directory}/bin", "*",
									 SearchOption.AllDirectories);
			return new JsonResult(filePaths);
		}
		[HttpGet]
		[Route("/positions")]
		public IActionResult Positions()
		{
			var positions = new List<string>();
			using (SqlConnection connection = new SqlConnection("Server=DESKTOP-MBD1D44;Database=Homework14(1);" +
						"Trusted_Connection=True;MultipleActiveResultSets=true"))
			{
				SqlCommand command = new SqlCommand("select * from Position", connection);
				command.CommandType = CommandType.Text;
				connection.Open();
				var reader = command.ExecuteReader();
				while (reader.Read())
				{
					positions.Add(reader["PositionName"].ToString());
				}
			}
			return new JsonResult(positions.ToArray()); 
		}
		}
	}
