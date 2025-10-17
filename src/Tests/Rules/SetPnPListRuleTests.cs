using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Rules
{
	[TestClass]
	public class SetListRuleTests
	{
		#region Test Setup/CleanUp
		[ClassInitialize]
		public static void Initialize(TestContext testContext)
		{
			// This runs on class level once before all tests run
		}

		[ClassCleanup]
		public static void Cleanup(TestContext testContext)
		{
			// This runs on class level once
		}

		[TestInitialize]
		public void Initialize()
		{
			using (var scope = new PSTestScope())
			{
				// Example
				// scope.ExecuteCommand("cmdlet", new CommandParameter("param1", prop));
			}
		}

		[TestCleanup]
		public void Cleanup()
		{
			using (var scope = new PSTestScope())
			{
				try
				{
					// Do Test Cleanup - Note, this runs PER test
				}
				catch (Exception)
				{
					// Describe Exception
				}
			}
		}
		#endregion

		#region Scaffolded Cmdlet Tests
		//TODO: This is a scaffold of the cmdlet - complete the unit test
		//[TestMethod]
		public void SetPnPListRuleTest()
		{
			using (var scope = new PSTestScope(true))
			{
				// Complete writing cmd parameters

				// From Cmdlet Help: The ID, Title or Url of the list.
				var list = "";
				// From Cmdlet Help: The ID or Title of the rule to update.
				var identity = "";

				var results = scope.ExecuteCommand("Set-PnPListRule",
					new CommandParameter("List", list),
					new CommandParameter("Identity", identity));
				
				Assert.IsNotNull(results);
			}
		}
		#endregion
	}
}
