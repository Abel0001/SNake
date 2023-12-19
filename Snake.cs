using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Snake{

    static bool isDead = false;
    static int snakeX = 0;
    static int snakeY = 0;
    public static List<int[]> snakeBody = new List<int[]>();

    static int direction = 0;
    public enum Directions{
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
     static int[] followerTemp = new int[2];

    // static Thread snakeInputThread = new Thread(new ThreadStart(SnakeInput));
   // static Thread snakeMoveThread = new Thread(new ThreadStart(SnakeMove));
   // static Thread snakeBodyThread = new Thread(new ThreadStart(UpdateBody));


    static void Reset(){
        points = 0;
        direction = 0;
        snakeBody = new List<int[]>();
        snakeMoveCounter = 0;
        snakeX = 0; snakeY = 0;
        addBody = false;
        foodLoc = new int[2];
        followerTemp = new int[2];
        Program.screen.TextDisplayAfter = new List<string>();
        Program.screen.TextDisplayBefore = new List<string>();
    }

    public static void SnakeInit(){
        Reset();
        Program.screen.TextDisplayBefore.Add("Irányítás 'W' 'A' 'S' 'D' gombbal");
        Program.screen.TextDisplayBefore.Add("\nPontok:" + points+"\n");
       // snakeMoveThread.Start();
        //snakeBodyThread.Start();

        snakeBody.Add(new int[] {5,5});
        SpawnWithValidFoodLoc(0);
        UpdateBody();
                SnakeInput();

    }
    
    public static void SnakeInput(){
        while(true){
            char input = 'o';
             if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            input = keyInfo.KeyChar;

        }

            
            switch (input){
                case 'w':
                    direction = (int)Directions.Up;
                //    SnakeMove();
                    break;
                case 's':
                    direction = (int)Directions.Down;
                //    SnakeMove();
                    break;
                case 'a':
                     direction = (int)Directions.Left;
                //    SnakeMove();
                    break;
                case 'd':
                    direction = (int)Directions.Right;
                //    SnakeMove();
                    break;
                case '1':
                System.Environment.Exit(1);
                break;
                case '2':
                addBody = true;
                break;
                default:
                break;
            }

        }
        
    }
    static bool addBody = false;
    private static void AddSnakeBodyPart()
    {
        snakeBody.Add(new int[] {-1,-1});
    }

    public static void SnakeMove(){
        snakeMoveCounter++;

        if(snakeMoveCounter == 1){
            snakeMoveCounter = 0;
        if(direction == (int)Directions.Left){
            if(snakeX > -1){
                snakeX += -1;
                
            }
            if(snakeX == -1){
                    snakeX = Program.screen.xSize - 1;

                }

        }
         if(direction == (int)Directions.Right){
            if(snakeX < Program.screen.xSize){
                snakeX += 1;
                
            }
            if(snakeX == Program.screen.xSize){
                    snakeX = 0;

                }

        }
        if(direction == (int)Directions.Up){
            if(snakeY > -1){
                snakeY += -1;
                
            }
            if(snakeY == -1){
                    snakeY = Program.screen.ySize - 1;

                }

        }
        if(direction == (int)Directions.Down){
            if(snakeY < Program.screen.ySize){
                snakeY += 1;
                
            }
            if(snakeY == Program.screen.ySize){
                    snakeY = 0;

                }

        }
            }
            
            
            }
    
    static int points = 0;
    static int snakeMoveCounter = 0;


    public static void SpawnWithValidFoodLoc(int iterator){
        SpawnFoodThing();
        if(foodLoc[0] == snakeBody[iterator][0] && foodLoc[1] == snakeBody[iterator][1]) SpawnWithValidFoodLoc(iterator);
    }
    public static async Task UpdateBody(){
       while(true){

        
        snakeBody[0] = new int[] {snakeX,snakeY};
            SnakeMove();

                Program.screen.ChangeCharacter(foodLoc[0],foodLoc[1],2);
        
        Program.screen.LockedList.Add(Program.screen.GetCorrespondingINum(foodLoc[0],foodLoc[1]));



        foreach (int[] bodyPart in snakeBody)
        {
            Program.screen.LockedList.Add(Program.screen.GetCorrespondingINum(bodyPart[0], bodyPart[1]));
        }


        Program.screen.ClearScreen();


        

        for(int i = snakeBody.Count - 1; i >= 0; i--){

                if(foodLoc[0] == snakeX && foodLoc[1] == snakeY) {addBody = true;
                 SpawnFoodThing();
                   points++;
                   Program.screen.TextDisplayBefore[1] ="\nPontok:" + points+"\n";}
            if(!(snakeBody[i][0] < 0 || snakeBody[i][1] < 0))  
            Program.screen.ChangeCharacter(snakeBody[i][0], snakeBody[i][1], 1);
            followerTemp = snakeBody[0];
            if(i == 0) {snakeBody[i] = new int[] {snakeX, snakeY}; continue;}
            
            int[] temp = snakeBody[i - 1];
            if(i == 1) {snakeBody[i] = followerTemp; continue;}
            snakeBody[i] = temp;
            if(snakeBody[0][0] == snakeBody[i][0] && snakeBody[0][1] == snakeBody[i][1]) {GameOver();
             }
        }
        if(addBody == true){AddSnakeBodyPart(); addBody = false;}
        Program.screen.LockedList = new List<int>();

        await Task.Delay(200); 
        }
        
    }
private static void GameOver()
{
    isDead = true;
    Program.screen.TextDisplayAfter.Add("Meghaltál!");
    Program.screen.RenderScreen();
    Task.Delay(3000).Wait(); 
    SnakeInit();
}
   static int[] foodLoc = new int[2];
    public static void SpawnFoodThing(){
        int x = RandomNumberGenerator.GetInt32(Program.screen.xSize);
        int y = RandomNumberGenerator.GetInt32(Program.screen.ySize);
        
        foodLoc = new int[] {x,y};
    }


}

