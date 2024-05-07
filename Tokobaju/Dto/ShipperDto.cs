using Microsoft.Net.Http.Headers;

namespace Tokobaju.Dto;

public class ShipperDto
{
    public string Name { get; set;} = "";
    public string Service { get; set;} = "";
    public int ShippingFee { get; set;} = 0;
}