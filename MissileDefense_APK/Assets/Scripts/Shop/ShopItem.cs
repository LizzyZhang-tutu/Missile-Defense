
/// <summary>
/// Shop Item Class.
/// </summary>
public class ShopItem {
    private string speed;
    private string rotate;
    private string model;
    private string price;
    private string id;

    public ShopItem(string speed, string rotate, string model, string price, string id)
    {
        this.Speed = speed;
        this.Rotate = rotate;
        this.Model = model;
        this.Price = price;
        this.Id = id;
    }

    //Properties.
    public string Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public string Rotate
    {
        get
        {
            return rotate;
        }

        set
        {
            rotate = value;
        }
    }

    public string Model
    {
        get
        {
            return model;
        }

        set
        {
            model = value;
        }
    }

    public string Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public override string ToString()
    {
        return string.Format(string.Format("speed:{0}, rotate:{1}, model:{2}, price:{3}", speed, rotate, model, price));
    }
}
