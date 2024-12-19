class Product()
{
    private int pk_pro;
    public int Pk_Pro
    {
        get
        {
            return pk_pro;
        }
    }

    private string name_pro;

    public string Name_pro
    {
        get
        {
            return name_pro;
        }
        set
        {
            name_pro = value;
        }
    }

    private int quantity_pro;

    private double price_pro;

    private Category category;
}