using System.Collections;

/// <summary>
using System.Collections.Generic;
using System.Xml;
/// Shopitem data store and read.
/// </summary>
public class ShopData {

    //to store all shopitems' info.
    public List<ShopItem> shopList = new List<ShopItem>();

    //to store shopitems' purchase status.
    public List<int> shopState = new List<int>();
    //gold count.
    public int goldCount;
    //best score.
    public int bestScore;
	
    /// <summary>
    /// Read xml file by path.
    /// </summary>
    /// <param name="path"></param>
    public void ReadXmlByPath(string path)
    {
        XmlDocument doc = new XmlDocument();
        //doc.Load(path);
        doc.LoadXml(path);
        XmlNode root = doc.SelectSingleNode("Shop");
        XmlNodeList nodeList = root.ChildNodes;
        //Read item's data.
        foreach (XmlNode node in  nodeList)
        {
            string speed = node.ChildNodes[0].InnerText;
            string rotate = node.ChildNodes[1].InnerText;
            string model = node.ChildNodes[2].InnerText;
            string price = node.ChildNodes[3].InnerText;
            string id = node.ChildNodes[4].InnerText;

            //store them in a shopitem list.
            ShopItem item = new ShopItem(speed, rotate, model, price, id);
            shopList.Add(item);
        }
    }

    public void ReadItemData(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        goldCount = int.Parse(nodeList[0].InnerText);
        bestScore = int.Parse(nodeList[1].InnerText);

        //Read shopitems' purchase status.
        for (int i = 2; i < 6; i++)
        {
            shopState.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// Update XML file.
    /// </summary>
    /// <param name="path"> storing path.</param>
    /// <param name="key"> node name.</param>
    /// <param name="value"> storing value.</param>
    public void UpdateXMLData(string path, string key, string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;

        foreach(XmlNode node in nodeList)
        {
            if(node.Name == key)
            {
                node.InnerText = value;
                doc.Save(path);
            }
        }
    }
}
