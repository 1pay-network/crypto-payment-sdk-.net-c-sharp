namespace Demo1Pay;

public class PaymentResponse
{
    public string? hash { get; set; }
    public bool success { get; set; }
    public string token { get; set; }
    public string network { get; set; }
    public string note { get; set; }
    public float amount { get; set; }

    public PaymentResponse(string? hash, bool success, string token, string network, string note, float amount)
    {
        this.hash = hash;
        this.success = success;
        this.token = token;
        this.network = network;
        this.note = note;
        this.amount = amount;
    }

    public PaymentResponse(Dictionary<string, string> dict) : this(
        dict.GetValueOrDefault("hash", null),
        Convert.ToBoolean(dict.GetValueOrDefault("success", "false")),
        dict["token"],
        dict["network"],
        dict["note"],
        (float) Convert.ToDouble(dict["amount"])
    )
    {
    }

    public static Dictionary<string, string> QueryToDict(string queryString)
    {
        if (queryString.StartsWith("?"))
            queryString = queryString.Substring(1);
        return queryString.Split("&").ToDictionary(key => key.Split("=")[0], value => value.Split("=")[1]);
    }
}