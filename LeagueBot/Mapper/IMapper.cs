namespace LeagueBot.Mapper
{
    public interface IMapper<T, TD>
    {

        public T ToSource(TD destination);

        public TD ToDestination(T source);
        
    }
}