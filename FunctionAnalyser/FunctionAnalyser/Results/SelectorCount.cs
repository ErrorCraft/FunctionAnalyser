namespace ErrorCraft.PackAnalyser.Results {
    public class SelectorCount {
        public int NearestPlayer { get; set; }
        public int AllPlayers { get; set; }
        public int RandomPlayer { get; set; }
        public int AllEntities { get; set; }
        public int CurrentEntity { get; set; }

        public static SelectorCount operator +(SelectorCount a, SelectorCount b) {
            return new SelectorCount() {
                NearestPlayer = a.NearestPlayer + b.NearestPlayer,
                AllPlayers = a.AllPlayers + b.AllPlayers,
                RandomPlayer = a.RandomPlayer + b.RandomPlayer,
                AllEntities = a.AllEntities + b.AllEntities,
                CurrentEntity = a.CurrentEntity + b.CurrentEntity
            };
        }
    }
}
