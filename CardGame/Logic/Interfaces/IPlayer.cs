namespace CardGame.Logic.Interfaces
{
    internal interface IPlayer
    {
        string Name { get; }
        Player.GameStatus Status { get; set; }
    }
}