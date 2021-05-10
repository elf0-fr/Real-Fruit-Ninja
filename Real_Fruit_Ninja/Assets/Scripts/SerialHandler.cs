using System;
using System.IO.Ports;
using UnityEngine;

public class SerialHandler : MonoBehaviour
{

    private SerialPort _serial;
    [Header("Arduino")]
    // Common default serial device on a Linux machine
    [SerializeField]
    private string serialPort = "/dev/ttyACM0";
    [SerializeField]
    private int baudrate = 115200;

    [Header("Event")]
    [SerializeField]
    private GameEvent oignonInputEvent;
    [SerializeField]
    private FruitType_gameEvent fruitInputEvent;

    // Start is called before the first frame update
    void Start()
    {
        _serial = new SerialPort(serialPort, baudrate);
        // Once configured, the serial communication must be opened just like a file : the OS handles the communication.
        _serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent blocking if no message is available as we are not doing anything else
        // Alternative solutions : set a timeout, read messages in another thread, coroutines, futures...
        if (_serial.BytesToRead <= 0) return;

        var message = _serial.ReadLine();

        // Arduino sends "\r\n" with println, ReadLine() removes Environment.NewLine which will not be 
        // enough on Linux/MacOS.
        if (Environment.NewLine == "\n")
        {
            message = message.Trim('\r');
        }

        switch (message)
        {
            case "1": //Pomme
                fruitInputEvent.Raise(FruitType.APPLE);
                break;
            case "2": //Banane
                fruitInputEvent.Raise(FruitType.BANANA);
                break;
            case "3": //Carotte
                fruitInputEvent.Raise(FruitType.CARROT);
                break;
            case "4": //Poire
                fruitInputEvent.Raise(FruitType.PEAR);
                break;
            case "5": //Tomate
                fruitInputEvent.Raise(FruitType.TOMATO);
                break;
            case "6": //Oignon
                oignonInputEvent.Raise();
                break;
            default:
                Debug.LogError("Input not recognize");
                break;
        }
    }

    private void OnDestroy()
    {
        _serial.Close();
    }
}
