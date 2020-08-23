namespace LeagueBot.Mapper
{
    public interface IMapper<T, TD>
    {

        public T ToSource(TD rectangle);

        public TD ToDestination(T source);
        
    }
}