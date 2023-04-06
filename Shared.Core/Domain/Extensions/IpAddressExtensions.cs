using System.Net;
using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Extensions;

public static class IpAddressExtensions
{
    public static bool InRange(this string address,string startIpAddr, string endIpAddr)
    {
        if (!address.IsValidIp())
            return false;
        
        long ipStart = BitConverter.ToInt32(IPAddress.Parse(startIpAddr).GetAddressBytes().Reverse().ToArray(), 0);

        long ipEnd = BitConverter.ToInt32(IPAddress.Parse(endIpAddr).GetAddressBytes().Reverse().ToArray(), 0);

        long ip = BitConverter.ToInt32(IPAddress.Parse(address).GetAddressBytes().Reverse().ToArray(), 0);

        return ip >= ipStart && ip <= ipEnd; //edited
    }

    public static string? Ip(this  HttpRequest request)
    {
        return request.HttpContext.Connection.RemoteIpAddress?.ToString();
    }
    public static bool IsValidIp(this string address,out IPAddress? ipAddress)
    {
        return IPAddress.TryParse(address, out  ipAddress);
    }
    
    
    public static bool IsValidIp(this string address)
    {
        return IPAddress.TryParse(address, out var ipAddress);
    }
  
    
}