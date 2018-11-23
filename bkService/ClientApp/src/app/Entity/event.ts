export class Event {
  id: number;
  gameId: number;
  eventTitle: string;
  status: string;
  matchTime: string; 
  team1: string;
  team2: string;
  score: string;

  outcomes: Outcomes;

  get SportName(): string {

    var result: string;

    if (this.gameId == 33) {
      result = "Football";
    }

    if (this.gameId == 31) {
      result = "Hockey";
    }


    return result;
  }
}

export class Outcomes {
  team1: number;
  team2: number;
  x: number;
}
