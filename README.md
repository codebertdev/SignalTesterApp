# SignalTesterApp
The SignalTesterApp is a decoder system designed to process and interpret typical low-level sensor data formats. It currently reads input from a `sample_data.csv` file, where each row represents a specific data encoding type (e.g., binary-coded decimal, two’s complement, ADC readings, etc.). The application decodes each input into human-readable values and logs the results.

Decoded entries are stored in a MySQL database for persistence and later analysis. A built-in WinForms graphical user interface allows users to view and filter decoded records conveniently, providing an easy way to inspect historical data. This setup simulates a simplified data acquisition and monitoring pipeline, which can be expanded in the future for real-time or embedded applications.


###  Setup Requirements

- A running and configured MySQL server
- Modify the `Appsettings.json` file to replace `password=YOURPASS` with your actual MySQL password
- Also update the MySQL credentials in `SignalTesterApp\SignalTesterApp.GUI\Form1.cs` if you're using the GUI

### More About the Decoding Process

The decoding logic is based on well-known data formats:

- **BCD (Binary-Coded Decimal):** https://en.wikipedia.org/wiki/Binary-coded_decimal  
- **Two's Complement:** https://en.wikipedia.org/wiki/Two%27s_complement  
- **Bitfield:** https://en.wikipedia.org/wiki/Bit_field  
- **ADC (Analog to Digital Conversion):** https://en.wikipedia.org/wiki/Analog-to-digital_converter  
- **Multi (HighByte + LowByte combination):** https://en.wikipedia.org/wiki/Endianness  

---

## Architecture

![Architecture Diagram](media/signaltester_architecture.png)
*Data flow from CSV to decoder, database, and GUI display*


[Open diagram directly if not working](media/signaltester_architecture.png)


##  Tech Used

- **Languages & Frameworks:** C#, .NET 8
- **Database:** MySQL
- **Testing Framework:** XUnit
- **Development Environment:** Visual Studio, .NET CLI

---
## Future Improvements

- **Real-Time Sensor Support:** Extend the system to handle real-time data input from sensors (e.g., serial, USB, or network sources).
- **Web-Based Dashboard (Optional):** In addition to the existing WinForms GUI, adding a web dashboard (e.g., using ASP.NET Core or React) for remote access and monitoring.
- **Authentication & User Roles:** Secure database access and GUI interactions with login functionality and role-based permissions.
- **Advanced CSV Handling:** Improve CSV validation, error reporting, and add automatic correction suggestions.
- **Docker Support:** Containerize the application for easier deployment using Docker.
- **Localization:** Introduce multilingual support (e.g., English and Hungarian) in both CLI and GUI.
 
---
## Lessons Learned

This project helped deepen my understanding of core .NET topics, particularly file handling, interface usage, and unit testing.

Since this is my first project on GitHub, I also learned a lot about using Git – including branching, committing, rebasing, and managing remote repositories – which greatly improved my confidence in version control and collaboration workflows.
