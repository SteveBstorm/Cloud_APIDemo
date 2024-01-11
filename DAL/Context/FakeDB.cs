using DAL.Entities;

namespace DAL.Context
{
    public static class FakeDB
    {

        public static List<User> Users { get; set; } = new List<User>()
        {
            new User(1, "Pol@Pol.be", "Test123=", "PolLaMenace"),
            new User(2, "Alice@Alice.be", "Alice123=", "AliceLaBelle"),
            new User(3, "Bob@Bob.be", "Bob123=", "BobLeBras"),
            new User(4, "Caroline@Caroline.be", "Caroline123=", "CarolineLaChouette"),
            new User(5, "David@David.be", "David123=", "DavidLeGrand"),
            new User(6, "Eve@Eve.be", "Eve123=", "EveLaFolle"),
            new User(7, "Fabien@Fabien.be", "Fabien123=", "FabienLeFort"),
            new User(8, "Gabrielle@Gabrielle.be", "Gabrielle123=", "GabrielleLaGentille"),
            new User(9, "Hugo@Hugo.be", "Hugo123=", "HugoLeHéros")
        };

    }
}
