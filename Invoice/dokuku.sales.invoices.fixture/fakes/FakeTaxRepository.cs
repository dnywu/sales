using dokuku.sales.invoices.domain;
namespace dokuku.sales.invoices.fixture.fakes
{
    public class FakeTaxRepository : ITaxRepository
    {
        public Tax FindByCode(string taxCode, string ownerId)
        {
            if(taxCode == "PPN")
                return new Tax("PPN", 10);

            return new Tax("NONE", 0);
        }
    }
}