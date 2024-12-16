class Book() 
{
    private string title;
    string Title {
        get{
            return title;
        }
        set{
            title = value;
        }
    }

    private DateTime publication_date;
    DateTime PublicationDate {
        get{
            return publication_date;
        }
        set {
            publication_date = value;
        }
    }
     

    Author author;

    public void getInfos(){
        Console.WriteLine($"title: {Title}, publication date: {PublicationDate}, author: {author.getFullName}.");
    }


}