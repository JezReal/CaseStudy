var peripherals = new List<Peripheral>();

mainMenu();

void mainMenu()
{
    Console.WriteLine("ABC Company Inventory System");
    Console.WriteLine();
    Console.WriteLine("[1] Add record");
    Console.WriteLine("[2] View record");
    Console.WriteLine("[3] Edit record");
    Console.WriteLine("[4] Delete record");
    Console.WriteLine("[5] Report");
    Console.WriteLine("[0] Exit");

    Console.Write("What would you like to do?: ");

    var mainOption = -1;

    try
    {
        mainOption = Convert.ToInt16(Console.ReadLine());
    }
    catch (FormatException _)
    {
        // leave this empty 
    }

    if (mainOption == 0)
    {
        Console.WriteLine("Thank you for using the app!\n");
        return;
    }
    else if (mainOption < 1 || mainOption > 5)
    {
        Console.WriteLine("Invalid input!\n");
        mainMenu();
    }
    else
    {
        subMenu(mainOption);
    }
}

void subMenu(int mainOption)
{
    switch (mainOption)
    {
        case 1:
            {
                Console.WriteLine("Add record \n");
                Console.WriteLine("[a] Add peripheral");
                Console.WriteLine("[b] Add peripheral to employee");
                Console.Write("What would you like to do?: ");

                var option = Console.ReadLine()?.ToLower()[0];

                if (option == null)
                {
                    Console.WriteLine("Invalid input!\n");
                    subMenu(mainOption);
                }

                if (option != 'a' && option != 'b')
                {
                    Console.WriteLine("Invalid input! \n");
                    subMenu(mainOption);
                }

                addPeripheral(option!.Value);

                break;
            }
        case 2:
            {
                Console.WriteLine("[a] View all peripherals");
                Console.WriteLine("[b] View available peripherals");
                Console.WriteLine("[c] View not available peripherals");
                Console.WriteLine("[d] View all input devices");
                Console.WriteLine("[e] View all output devices");
                Console.WriteLine("[f] View all input/output devices");
                Console.WriteLine("[g] View all employees");

                var option = Console.ReadLine()?.ToLower()[0];

                if (option == null)
                {
                    Console.WriteLine("Invalid input!\n");
                    subMenu(mainOption);
                }

                if (option != 'a' && option != 'b' && option != 'c' && option != 'd' && option != 'e' && option != 'f' && option != 'g')
                {
                    Console.WriteLine("Invalid input! \n");
                    subMenu(mainOption);
                }

                viewRecords(option!.Value);
                break;
            }
        case 3:
            editPeripheral();
            break;
        case 4:
            deletePeripheral();
            break;
        case 5:
            {
                Console.WriteLine("[a] Number of available peripherals");
                Console.WriteLine("[b] Number of assigned peripherals");
                Console.WriteLine("[c] Number of input device");
                Console.WriteLine("[d] Number of output device");
                Console.WriteLine("[e] Number of employees with peripherals assigned");

                generateReport();
            }

            break;
    }
}

void addPeripheral(char option)
{
    switch (option)
    {
        case 'a':
            {
                var isTypeValid = false;

                Console.WriteLine("Add peripheral\n");

                Console.Write("Enter peripheral name: ");
                var name = Console.ReadLine();

                if (name == null)
                {
                    Console.WriteLine("Invalid input!");
                    addPeripheral(option);
                }

                int type = -1;

                while (!isTypeValid)
                {
                    Console.Write("[1] Input device \n[2] Output device \n[3]Input/Output device \nEnter peripheral type: ");
                    type = Convert.ToInt32(Console.ReadLine());

                    if (type < 1 || type > 3)
                    {
                        Console.WriteLine("Invalid input!\n");
                    }
                    else
                    {
                        isTypeValid = true;
                    }
                }

                Console.Write("Enter employee name to assign the peripheral: ");
                var assignedTo = Console.ReadLine();


                var peripheral = new Peripheral(name!, type.IntToPeripheralType(), assignedTo);

                peripherals.Add(peripheral);

                peripherals.ForEach((item) =>
                {
                    Console.WriteLine(item.ToString());
                });

                Console.WriteLine();
                mainMenu();

                break;
            }
        case 'b':
            {
                Console.WriteLine("Add peripheral to employee\n");

                Console.Write("Enter peripheral code: ");

                long code = -1;

                try
                {
                    code = Convert.ToInt64(Console.ReadLine());
                }
                catch (FormatException _)
                {
                    Console.WriteLine("Invalid peripheral code!");
                    addPeripheral(option);
                }

                var peripheral = peripherals.FirstOrDefault((item) =>
                {
                    return item.Code == code;
                });

                if (peripheral == null)
                {
                    Console.WriteLine($"Peripheral with code {code} not found");
                    addPeripheral(option);
                }

                Console.Write("Enter employee name to assign the peripheral to: ");
                var name = Console.ReadLine();

                peripheral!.AssignedTo = name;

                Console.WriteLine($"Peripheral {code} successfully assigned to {name} \n");

                mainMenu();
                break;
            }
    }
}

void viewRecords(char option)
{
    Console.WriteLine("View records \n");

    switch (option)
    {
        case 'a':
            Console.WriteLine("All peripherals: \n");
            peripherals.ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());
            });
            break;
        case 'b':
            Console.WriteLine("Available peripherals: \n");

            var availablePeripherals = new List<Peripheral>(peripherals.Where((peripheral) =>
            {
                return peripheral.AssignedTo == "";
            }));

            availablePeripherals.ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());

            });

            break;
        case 'c':
            Console.WriteLine("Unavailable peripherals: \n");
            var notAvailablePeripherals = new List<Peripheral>(peripherals.Where((peripheral) =>
           {
               return peripheral.AssignedTo != "";
           }));

            notAvailablePeripherals.ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());

            });

            break;
        case 'd':
            Console.WriteLine("Input devices: \n");

            var inputDevices = new List<Peripheral>(peripherals.Where(peripheral =>
            {
                return peripheral.Type == PeripheralType.INPUT;
            }));

            inputDevices.ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());
            });

            break;
        case 'e':
            Console.WriteLine("Output devices: \n");

            peripherals.Where((peripheral) =>
            {
                return peripheral.Type == PeripheralType.OUTPUT;
            })
            .ToList()
            .ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());
            });

            break;
        case 'f':
            Console.WriteLine("Input/Output devices: \n");

            peripherals.Where((peripheral) =>
            {
                return peripheral.Type == PeripheralType.BOTH;
            })
            .ToList()
            .ForEach((peripheral) =>
            {
                Console.WriteLine(peripheral.ToString());
            });

            break;
        case 'g':
            Console.WriteLine("All employees: \n");

            peripherals.Where((peripheral) =>
            {
                return peripheral.AssignedTo != null;
            })
            .ToList()
            .Select(peripheral => peripheral.AssignedTo)
            .Distinct()
            .ToList()
            .ForEach((employee) =>
            {
                Console.WriteLine(employee);
            });

            break;
    }

    mainMenu();
}

void editPeripheral()
{
    Console.Write("Enter peripheral code: ");

    long code = -1;

    try
    {
        code = Convert.ToInt64(Console.ReadLine());
    }
    catch (FormatException _)
    {
        Console.WriteLine("Invalid peripheral code!");
        editPeripheral();
    }

    var peripheral = peripherals.First((peripheral) =>
    {
        return peripheral.Code == code;
    });

    if (peripheral == null)
    {
        Console.WriteLine($"Peripheral with code {code} not found");
        editPeripheral();
    }

    Console.Write("Enter name: ");
    var name = Console.ReadLine();

    if (name == null)
    {
        Console.WriteLine("Invalid input!");
        editPeripheral();
    }

    var isCodeValid = false;
    int type = -1;

    while (!isCodeValid)
    {
        Console.Write("[1] Input device \n[2] Output device \n[3]Input/Output device \nEnter peripheral type: ");
        type = Convert.ToInt32(Console.ReadLine());

        if (type < 1 || type > 3)
        {
            Console.WriteLine("Invalid input!\n");
        }
        else
        {
            isCodeValid = true;
        }
    }

    Console.Write("Enter employee to assign the peripheral to: ");
    var assignedTo = Console.ReadLine();

    peripheral!.Name = name!;
    peripheral.Type = type.IntToPeripheralType();
    peripheral.AssignedTo = assignedTo;

    Console.WriteLine($"Peripheral with code {code} has been successfully updated \n");
    mainMenu();
}

void deletePeripheral()
{
    Console.Write("Enter peripheral code: ");
    long code = -1;

    try
    {
        code = Convert.ToInt64(Console.ReadLine());
    }
    catch (FormatException _)
    {
        Console.WriteLine("Invalid peripheral code!");
        deletePeripheral();
    }

    var peripheral = peripherals.First((peripheral) =>
    {
        return peripheral.Code == code;
    });

    if (peripheral == null)
    {
        Console.WriteLine($"Peripheral with code {code} not found");
        deletePeripheral();
    }

    peripherals.Remove(peripheral!);

    Console.WriteLine($"Peripheral with code {code} has been successfully deleted");

    mainMenu();
}

void generateReport()
{
    Console.WriteLine("Report: \n");

    var availablePeripherals = peripherals.Where((peripheral) =>
        {
            return peripheral.AssignedTo == null;
        })
        .ToList()
        .Count();

    var assignedPeripherals = peripherals.Where((peripheral) =>
        {
            return peripheral.AssignedTo != null;
        })
        .ToList()
        .Count();

    var inputDevices = peripherals.Where((peripheral) =>
        {
            return peripheral.Type == PeripheralType.INPUT;
        })
        .ToList()
        .Count();

    var outputDevices = peripherals.Where((peripheral) =>
        {
            return peripheral.Type == PeripheralType.OUTPUT;
        })
        .ToList()
        .Count();

    var employeesWithPeripherals = peripherals.Where((peripheral) =>
        {
            return peripheral.AssignedTo != null;
        })
        .ToList()
        .Select(peripheral => peripheral.AssignedTo)
        .Distinct()
        .ToList()
        .Count();

    Console.WriteLine($"Number of available peripherals: {availablePeripherals}");
    Console.WriteLine($"Number of assigned peripherals: {assignedPeripherals}");
    Console.WriteLine($"Number of input devices: {inputDevices}");
    Console.WriteLine($"Number of output devices: {outputDevices}");
    Console.WriteLine($"Number of employees with peripherals assigned: {employeesWithPeripherals} \n");

    mainMenu();
}