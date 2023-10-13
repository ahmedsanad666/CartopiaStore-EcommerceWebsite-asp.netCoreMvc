namespace CartopiaStore.Models;

public class User
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string maidenName { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string birthDate { get; set; }
    public string image { get; set; }
    public string bloodGroup { get; set; }
    public int height { get; set; }
    public double weight { get; set; }
    public string eyeColor { get; set; }
    public Hair hair { get; set; }
    public string domain { get; set; }
    public string ip { get; set; }
    public Address address { get; set; }
    public string macAddress { get; set; }
    public string university { get; set; }
    public Bank bank { get; set; }
    public Company company { get; set; }
    public string ein { get; set; }
    public string ssn { get; set; }
    public string userAgent { get; set; }
}

public class Hair
{
    public string color { get; set; }
    public string type { get; set; }
}

public class Address
{
    public string address { get; set; }
    public string city { get; set; }
    public Coordinates coordinates { get; set; }
    public string postalCode { get; set; }
    public string state { get; set; }
}

public class Bank
{
    public string cardExpire { get; set; }
    public string cardNumber { get; set; }
    public string cardType { get; set; }
    public string currency { get; set; }
    public string iban { get; set; }
}

public class Company
{
    public Address address { get; set; }
    public string department { get; set; }
    public string name { get; set; }
    public string title { get; set; }
}

public class Coordinates
{
    public double lat { get; set; }
    public double lng { get; set; }
}
