namespace demo.ioc._01_NonDI
{
    public class TheService
    {
        public void Work()
        {
            
        }
    }

    public class TheController
    {
        readonly TheService _theService = new TheService();
        //validator
        //logging

        public void DoStuff()
        {
            _theService.Work();
        }
    }
}