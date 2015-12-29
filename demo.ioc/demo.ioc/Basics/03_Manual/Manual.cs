namespace demo.ioc._03_Manual
{
    public class TheService
    {
        public void Work()
        {
            
        }
    }

    public class TheController
    {
        private readonly TheService _theService;

        public TheController(TheService theService)
        {
            _theService = theService;
        }

        public void DoStuff()
        {
            _theService.Work();
        }
    }

    public class Manual
    {
        public void Main()
        {
            //this is all it is in a nut shell
            var c = new TheController(new TheService());
            c.DoStuff();
























            //but about reality?
            // this could get really old, really quick.
            var db = new DatabaseSettings();
            var repo = new Repository(db);
            var s1 = new SomeService(repo);
            var s2 = new AnotherService(repo, new ValidationService());
            var tt = new TheRealWorker(s1, s2);

        }
    }


#region "abc"

    public class DatabaseSettings
    {
        
    }
    public class Repository
    {
        public Repository(DatabaseSettings settings)
        {
            
        }
    }

    public class SomeService
    {
        public SomeService(Repository repository)
        {
            
        }
    }

    public class AnotherService
    {
        public AnotherService(Repository rep, ValidationService srv)
        {

        }
    }

    public class ValidationService
    {
        
    }

    public class TheRealWorker
    {
        private readonly SomeService _ss;
        private readonly AnotherService _asa;

        public TheRealWorker(SomeService ss, AnotherService asa)
        {
            _ss = ss;
            _asa = asa;
        }

        public void Yo()
        {
            
        }
    }

    #endregion
}