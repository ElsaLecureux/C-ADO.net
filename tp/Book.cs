class Book() 
{
    private string title;
    public string Title {
        get{
            return title;
        }
        set{
            title = value;
        }
    }

    private int publication_date;
    public int PublicationDate {
        get{
            return publication_date;
        }
        set {
            publication_date = value;
        }
    }
     

    public Author author;

    public void getInfos(){
        Console.WriteLine($"title: {Title}, publication date: {PublicationDate} author: {author.FirstName} {author.LastName}");
        //author.getFullName();
    }


}