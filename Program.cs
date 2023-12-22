using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ScreenLib;
internal class Program
{

    public static Screen screen = new Screen(15, 15);
     
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        screen.TextDisplayBefore.Add("A '9'-es gomb lenyomásával kezdődik a snake játék");
        
        screen.InitScreen();
        Commands();
        


    }
        static int xTest = 0;
        static int yTest = 0;
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
            Snake.SnakeInit();
            break;
            default:
                Commands();
            break;
            
        }
    }
    
    
}