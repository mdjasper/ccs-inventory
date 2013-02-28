/// <summary>
/// Password Class - hashes password/salt combinations
/// </summary>
public static class Password
{
	public static string Hash(string password, string salt)
	{
        var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        var bs = System.Text.Encoding.UTF8.GetBytes(password + salt);
        bs = x.ComputeHash(bs);
        var s = new System.Text.StringBuilder();
        foreach (var b in bs)
        {
            s.Append(b.ToString("x2").ToLower());
        }
        return s.ToString();
	}
}