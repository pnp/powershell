namespace PnP.PowerShell.Commands.Enums
{
	/// <summary>
	/// Operator types for Microsoft Search result type rules.
	/// Maps to the N code in the GCS API rule operator.
	/// </summary>
	public enum SearchResultTypeRuleOperatorType
	{
		Equals = 1,
		NotEquals = 2,
		Contains = 3,
		DoesNotContain = 4,
		LessThan = 5,
		GreaterThan = 6,
		StartsWith = 7
	}
}
