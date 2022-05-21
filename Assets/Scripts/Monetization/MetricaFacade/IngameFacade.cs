using System.Collections.Generic;
using Game.States;

namespace Monetization.Metrica
{
    public class IngameFacade : MetricaFacade
    {
        /// <summary>
        /// Событие старта уровня
        /// </summary>
        /// <param name="levelNumber">Если уровни идут по кругу - параметр не должен продолжать расти, он показывает номер конкретного уровня который был запущен</param>
        /// <param name="levelCount">Порядковый номер игры для пользователя (Показывает какое количество игр пользователь сыграл за все время жизни) - Начинается с 1</param>
        public void SendLevelStart(int levelNumber, string levelName, int levelCount)
        {
            Parameters = new Dictionary<string, object>
            {
                { "level_number", levelNumber },
                { "level_name", levelName },
                { "level_count", levelCount },
                { "level_diff", "easy" },
                { "level_loop", 1 },
                { "level_random", 0 },
                { "level_type", "normal" }
            };

            SendHard("level_start");
        }

        /// <summary>
        /// Событие завершения уровня
        /// </summary>
        public void SendLevelFinish(EndGameStats gameStats, string levelName)
        {
            Parameters = new Dictionary<string, object>
            {
                { "level_number",  gameStats.LevelNumber},
                { "level_name", levelName },
                { "level_count", gameStats.LevelCount },
                { "level_diff", "easy" },
                { "level_loop", 1 },
                { "level_random", 0 },
                { "level_type", "normal" },
                { "result", gameStats.Result },
                { "time", gameStats.PassedTime },
                { "progress", gameStats.Progress },
                { "continue", 0 }
            };

            SendHard("level_finish");
        }
    }
}