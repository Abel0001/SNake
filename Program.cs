using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
internal class Program
{
    static int snakeX = 0;
    static int snakeY = 0;
    public static List<int[]> snakeBody = new List<int[]>();
    static Screen screen = new Screen(15, 15);
     static int direction = 0;
    public enum Directions{
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        screen.TextDisplayBefore.Add("A '9'-es gomb lenyomásával kezdődik a snake játék");
        screen.InitScreen();
        Commands();
        
        
    }
        static int xTest = 0;
        static int yTest = 0;
        static int[] followerTemp = new int[2];
    public static void Commands(){

         char input = Console.ReadKey().KeyChar;

         switch (input){
            case '0':
                screen.ClearScreen();
                Commands();

            break;
            case '1':
                System.Environment.Exit(1);
                break;

            
            case 'w':
                if(yTest > 0) yTest--;
                screen.ClearScreen();
                screen.ChangeCharacter(xTest,yTest, 1);
                
                Commands();
            break;
            case 's':
                if(yTest < screen.ySize - 1) yTest++;
                screen.ClearScreen();
                screen.ChangeCharacter(xTest,yTest, 1);
                Commands();
            break;
            case 'a':
                if(xTest > 0) xTest--;
                screen.ClearScreen();
                screen.ChangeCharacter(xTest,yTest, 1);
                
                Commands();
            break;
            case 'd':
                if(xTest < screen.xSize - 1) xTest++;
                 screen.ClearScreen();

                screen.ChangeCharacter(xTest,yTest, 1);

                Commands();
            break;
            case '9':
            SnakeInit();
            break;
            default:
                Commands();
            break;
            
        }
    }
    static Thread snakeInputThread = new Thread(new ThreadStart(Program.SnakeInput));
    static Thread snakeMoveThread = new Thread(new ThreadStart(Program.SnakeMove));
    static Thread snakeBodyThread = new Thread(new ThreadStart(Program.UpdateBody));


    public static void SnakeInit(){
        screen.TextDisplayBefore[0] = "Irányítás 'W' 'A' 'S' 'D' gombbal";
        screen.TextDisplayBefore.Add("\nPontok:" + points+"\n");
        SpawnFoodThing();
        snakeInputThread.Start();
        snakeMoveThread.Start();
       snakeBodyThread.Start();
        snakeBody.Add(new int[] {5,5});
    }
    public static void SnakeInput(){
        while(true){
            char input = Console.ReadKey().KeyChar;
            
            switch (input){
                case 'w':
                    direction = (int)Directions.Up;
                    break;
                case 's':
                    direction = (int)Directions.Down;
                    break;
                case 'a':
                     direction = (int)Directions.Left;

                    break;
                case 'd':
              direction = (int)Directions.Right;
                    break;
                case '1':
                System.Environment.Exit(1);
                break;
                case '2':
                addBody = true;
                break;
            }
        }
        
    }
    static bool addBody = false;
    private static void AddSnakeBodyPart()
    {
        snakeBody.Add(new int[] {0,0});
    }

    public static void SnakeMove(){
            while(true){
        if(direction == (int)Directions.Left){
            if(snakeX > -1){
                snakeX += -1;
                
            }
            if(snakeX == -1){
                    snakeX = screen.xSize - 1;

                }
            Thread.Sleep(300);

        }
         if(direction == (int)Directions.Right){
            if(snakeX < screen.xSize){
                snakeX += 1;
                
            }
            if(snakeX == screen.xSize){
                    snakeX = 0;

                }
            Thread.Sleep(300);

        }
        if(direction == (int)Directions.Up){
            if(snakeY > -1){
                snakeY += -1;
                
            }
            if(snakeY == -1){
                    snakeY = screen.ySize - 1;

                }
            Thread.Sleep(300);

        }
        if(direction == (int)Directions.Down){
            if(snakeY < screen.ySize){
                snakeY += 1;
                
            }
            if(snakeY == screen.ySize){
                    snakeY = 0;

                }
            Thread.Sleep(300);

        }
            }
    }
    static int points = 0;
    public static void UpdateBody(){
       while(true){


        snakeBody[0] = new int[] {snakeX,snakeY};
        

        screen.ClearScreen();
        screen.ChangeCharacter(foodLoc[0],foodLoc[1],2);

        for(int i = snakeBody.Count - 1; i >= 0; i--){
                if(foodLoc[0] == snakeX && foodLoc[1] == snakeY) {addBody = true; SpawnFoodThing(); points++; screen.TextDisplayBefore[1] ="\nPontok:" + points+"\n";}

            screen.ChangeCharacter(snakeBody[i][0], snakeBody[i][1], 1);
            followerTemp = snakeBody[0];
            if(i == 0) {snakeBody[i] = new int[] {snakeX, snakeY}; continue;}

            int[] temp = snakeBody[i - 1];
            if(i == 1) {snakeBody[i] = followerTemp; continue;}
            snakeBody[i] = temp;
        }
        
        if(addBody == true){AddSnakeBodyPart(); addBody = false;}

        Thread.Sleep(300);

        }
        
    }

   static int[] foodLoc = new int[2];
    public static void SpawnFoodThing(){
        int x = RandomNumberGenerator.GetInt32(screen.xSize);
        int y = RandomNumberGenerator.GetInt32(screen.ySize);
        
        foodLoc = new int[] {x,y};
    }

    
}