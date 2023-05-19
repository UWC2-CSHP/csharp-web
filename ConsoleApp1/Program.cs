// File: Project1.cs
// Write a program to keep track of some inventory items as shown below.
// Hint: when creating arrays, as you get or read items into
// your array, then initialize each array spot before its use
// and place a counter or use your own Mylength to keep track
// of your array length as it is used.

using System;
struct ItemData
{
    public int itemIDNo;
    public string sDescription;
    public double dblPricePerItem;
    public int iQuantityOnHand;
    public double dblOurCostPerItem;
    public double dblValueOfItem;
}


class MyInventory
{
    public static void Main()
    {
        int icount = 0;
        ItemData[] itemprop = new ItemData[100];

        int optx = 1;
        while (optx != 6) // as long as no one Quits, continue the inventory update
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. Change and item");
            Console.WriteLine("3. Delete an item");
            Console.WriteLine("4. List all items in the database");
            Console.WriteLine("5. List items ordered by the user (please give quantity)");
            Console.WriteLine("6. Quit");
            Console.WriteLine();
            Console.Write("Please choose an option from the list(1, 2, 3, 4, 5, or 6):");


            string strx = Console.ReadLine();

            optx = int.Parse(strx);

            Console.WriteLine();

            switch (optx)
            {
                case 1: // add an item to the list	
                    {
                        Console.Write("Please enter item ID No. (3-digits)      :");
                        string strn = Console.ReadLine();
                        itemprop[icount].itemIDNo = int.Parse(strn);

                        Console.Write("Please enter an item description(3 words):");
                        itemprop[icount].sDescription = Console.ReadLine();

                        Console.Write("Please enter item price($)               :");
                        string strPrice = Console.ReadLine();
                        itemprop[icount].dblPricePerItem = double.Parse(strPrice);

                        Console.Write("Please enter quantity on hand            :");
                        string strQuantity = Console.ReadLine();
                        itemprop[icount].iQuantityOnHand = int.Parse(strQuantity);

                        Console.Write("Please enter our item cost               :");
                        string strCost = Console.ReadLine();
                        itemprop[icount].dblOurCostPerItem = double.Parse(strCost);

                        double dblvalue = (double)(itemprop[icount].dblPricePerItem * itemprop[icount].iQuantityOnHand);
                        itemprop[icount].dblValueOfItem = dblvalue;

                        icount = icount + 1;
                        Console.WriteLine("\n\n\n");
                        break;
                    }

                case 2: //change items in the list
                    {
                        Console.Write("Please enter an item ID No:");
                        string strchgid = Console.ReadLine();
                        int chgid = int.Parse(strchgid);
                        bool fFound = false;

                        for (int x = 0; x < icount; x++)
                        {
                            if (itemprop[x].itemIDNo == chgid)
                            {
                                fFound = true;
                                Console.Write("Please enter an item description(3 words):");
                                itemprop[x].sDescription = Console.ReadLine();

                                Console.Write("Please enter item price($)               :");
                                string strPrice = Console.ReadLine();
                                itemprop[x].dblPricePerItem = double.Parse(strPrice);

                                Console.Write("Please enter quantity on hand            :");
                                string strQuantity = Console.ReadLine();
                                itemprop[x].iQuantityOnHand = int.Parse(strQuantity);

                                Console.Write("Please enter our item cost               :");
                                string strCost = Console.ReadLine();
                                itemprop[x].dblOurCostPerItem = double.Parse(strCost);

                                double dblvalue = (double)(itemprop[x].dblPricePerItem * itemprop[x].iQuantityOnHand);
                                itemprop[x].dblValueOfItem = dblvalue;
                            }
                        }

                        if (!fFound)
                        {
                            Console.WriteLine("Item {0} not found", chgid);
                        }

                        //can continue the same procedure for other data items...
                        break;
                    }

                case 3: //delete items in the list
                    {
                        Console.Write("Please enter an item ID No:");
                        string strnewid = Console.ReadLine();
                        int newid = int.Parse(strnewid);
                        bool fDeleted = false;

                        for (int x = 0; x < icount; x++)
                        {
                            if (itemprop[x].itemIDNo == newid)
                            {
                                fDeleted = true;
                                // use y=x to start at the location where we found the item
                                for (int y = x; y < icount - 1; y++)
                                {
                                    itemprop[y] = itemprop[y + 1];
                                }
                                icount = icount - 1;
                                break;

                            }
                        }

                        if (fDeleted)
                        {
                            Console.WriteLine("Item deleted");
                        }
                        else
                        {
                            Console.WriteLine("Item {0} not found", newid);
                        }

                        // can continue the same procedure for other data items...
                        break;
                    }

                case 4:  //list all items in current database
                    {
                        Console.WriteLine("Item#  ItemID  Description           Price  QOH  Cost  Value");
                        Console.WriteLine("-----  ------  --------------------  -----  ---  ----  -----");
                        for (int x = 0; x < icount; x++)
                        {
                            Console.Write("{0,5}  ", x);
                            Console.Write("{0,6}  ", itemprop[x].itemIDNo);
                            Console.Write("{0,-20}  ", itemprop[x].sDescription);
                            Console.Write("{0,5}  ", itemprop[x].dblPricePerItem);
                            Console.Write("{0,3}  ", itemprop[x].iQuantityOnHand);
                            Console.Write("{0,4}  ", itemprop[x].dblOurCostPerItem);
                            Console.Write("{0,5}", itemprop[x].dblValueOfItem);
                            Console.WriteLine();
                        }

                        break;
                    }

                case 5: //list items for a given quantity so we check if we need to order some more
                        //list all items in current database
                    {
                        Console.Write("Please enter the lower limit quantity:");
                        string strquan = Console.ReadLine();
                        int iquan = int.Parse(strquan);

                        Console.WriteLine("Item#  ItemID  Description           Price  QOH  Cost  Value");
                        Console.WriteLine("-----  ------  --------------------  -----  ---  ----  -----");
                        for (int x = 0; x < icount; x++)
                        {
                            if (itemprop[x].iQuantityOnHand <= iquan)
                            {
                                Console.Write("{0,5}  ", x);
                                Console.Write("{0,6}  ", itemprop[x].itemIDNo);
                                Console.Write("{0,-20}  ", itemprop[x].sDescription);
                                Console.Write("{0,5}  ", itemprop[x].dblPricePerItem);
                                Console.Write("{0,3}  ", itemprop[x].iQuantityOnHand);
                                Console.Write("{0,4}  ", itemprop[x].dblOurCostPerItem);
                                Console.Write("{0,5}", itemprop[x].dblValueOfItem);
                                Console.WriteLine();
                            }
                        }

                        break;
                    }

                case 6: //quit the program
                    {
                        Console.Write("Are you sure that you want to quit(y/n)?");
                        string strresp = Console.ReadLine();

                        if (strresp.ToLower() != "y")
                        {
                            optx = 0;   //as long as it is not 6, the process is not breaking	
                        }
                        break;
                    }

                default:
                    {
                        Console.Write("Invalid Option, try again");
                        break;
                    }
            }
        }
    }
}