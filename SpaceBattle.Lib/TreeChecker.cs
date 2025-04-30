<<<<<<< HEAD
﻿namespace SpaceBattle.Lib;
using App;

public class TreeChecker : ITreeChecker
=======
namespace SpaceBattle.Lib;
using App;

public class TreeChecker: ITreeChecker
>>>>>>> 8a5f295 (ColCommand&Test)
{
    public void Check(int x, int y, int vel_x, int vel_y)
    {
        Ioc.Resolve<ICommand>("shipandtorpedoTreeCheckSet", x, y, vel_x, vel_y).Execute();
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8a5f295 (ColCommand&Test)
