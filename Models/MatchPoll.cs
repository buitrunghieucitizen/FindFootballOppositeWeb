using System;
using System.Collections.Generic;

namespace FindFootballOppsite.Models;

public partial class MatchPoll
{
    public int MatchId { get; set; }

    public int PlayerId { get; set; }

    public bool? IsAttending { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual User Player { get; set; } = null!;
}
