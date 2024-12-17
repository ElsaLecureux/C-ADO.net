class Author()
{
    private string firstName;
    public String FirstName {
        get{
            return firstName;
        }
        set{
            firstName = value;
        }
    }
    private string lastName;
    public string LastName{
        get{
            return lastName;
            } 
        set{
            lastName = value;
        }
    }

    public void getFullName(){
        Console.WriteLine($"author: {FirstName} {LastName}");
    }
}