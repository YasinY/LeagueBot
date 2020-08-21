namespace LeagueBot.Mapper
{
    public interface IMapper<T, TD>
    {

        public T ToSource(TD json);

        public TD ToDestination(T source);
        
    }
}