# SeleniumNUnitMailingTest  
  
  
 Тестовое задание на работу с Selenium, NUnit, NLog

![image](https://user-images.githubusercontent.com/33147329/129576938-a6a49aeb-0896-4885-aa54-5b4a4142a6e3.png)
![image](https://user-images.githubusercontent.com/33147329/129576970-4c9d902a-dbe9-4256-93b4-89846ae4fb39.png)

FAQ:

# Как запустить:  
Exe файл находится по пути: SeleniumNUnitMailingTest/UnitTests/bin/Debug/UnitTests.exe  
В папке Debug также находится файл Configuration.xml, отвечающий за настройку запуска тестов.  
Exe файл необходимо запустить через cmd. Можно указать путь до файла конфигурации  
(пример: "UnitTests.exe C:\Configuration.xml"). Если путь не будет указан, будет использован локальный файл конфигурации, находящийся в папке SeleniumNUnitMailingTest/UnitTests/bin/Debug.  

# Пример файла конфигурации:  
![image](https://user-images.githubusercontent.com/33147329/129579065-ec723657-d3c4-43f2-bdde-4d8ff46d2e84.png)

# Как настроить запуск тестов:  
В файле конфигурации необходимо указать логин и пароль для входа в почту Mail.ru (пока реализована только она).
В нем также перечислены :  
 Флаг, включено ли логирование;  
 Сервис почты (Пока только Mail.ru);  
 Тесты, которые будут запущены;  
 
# Куда пишутся логи:  
Все логи выполнения пишутся в папку SeleniumNUnitMailingTest/UnitTests/bin/Debug/logs

# Куда пишутся результаты тестов:  
Результат прогона тестов сохраняется в файле TestResult.xml в папке SeleniumNUnitMailingTest/UnitTests/bin/Debug

# Как включить/отключить логирование:  
Логирование можно вкл/выкл в файле конфигурации, проставляя в поле LoggingEnabled значения соответсвтующие true/false.  
