namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;

public class TransactionDto
{
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public DateTime dateTime { get; set; }
    
    public List<TariffPost> details { get; set; } = null!;

    public void setTariff(List<Tariff> selectedTariffs, double studentDiscount, double arrears)
    {
        details = new List<TariffPost>();
        foreach (var item in selectedTariffs)
        {
            var tariff = new TariffPost
            {
                tariffId = item.TariffId,
                discount = studentDiscount
            };

                tariff.arrears = arrears;

            
            details.Add(tariff);
        }
    }
}


