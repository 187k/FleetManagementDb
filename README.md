# 🚚 Система управления транспортными средствами, водителями и рейсами

## Информация

* [Введение](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%92%D0%B2%D0%B5%D0%B4%D0%B5%D0%BD%D0%B8%D0%B5.md)
* [Пакеты системы](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%9F%D0%B0%D0%BA%D0%B5%D1%82%D1%8B%20%D0%A1%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D1%8B.md)
* [Требования к системе](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%A2%D1%80%D0%B5%D0%B1%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F%20%D0%BA%20%D1%81%D0%B8%D1%81%D1%82%D0%B5%D0%BC%D0%B5.md)
* [Этапы разработки](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%AD%D1%82%D0%B0%D0%BF%D1%8B%20%D1%80%D0%B0%D0%B7%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%BA%D0%B8.md)
* [Документация для внедрения](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%94%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F%20%D0%B4%D0%BB%D1%8F%20%D0%B2%D0%BD%D0%B5%D0%B4%D1%80%D0%B5%D0%BD%D0%B8%D1%8F.md)
* [Документация для пользователей](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%94%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F%20%D0%B4%D0%BB%D1%8F%20%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D0%B5%D0%B9.md)
* [Документация для разработчиков](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%94%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B0%D1%86%D0%B8%D1%8F%20%D0%B4%D0%BB%D1%8F%20%D1%80%D0%B0%D0%B7%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D1%87%D0%B8%D0%BA%D0%BE%D0%B2.md)
* [План Внедрения](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%9F%D0%BB%D0%B0%D0%BD%20%D0%92%D0%BD%D0%B5%D0%B4%D1%80%D0%B5%D0%BD%D0%B8%D1%8F.md)
* [Интерфейс](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%98%D0%BD%D1%82%D0%B5%D1%80%D1%84%D0%B5%D0%B9%D1%81.md)
* [Руководство по установке](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%A0%D1%83%D0%BA%D0%BE%D0%B2%D0%BE%D0%B4%D1%81%D1%82%D0%B2%D0%BE%20%D0%BF%D0%BE%20%D1%83%D1%81%D1%82%D0%B0%D0%BD%D0%BE%D0%B2%D0%BA%D0%B5.md)
* [Тестирование](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%A2%D0%B5%D1%81%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5.md)
* [Анализ рисков](https://github.com/187k/FleetManagementDb/blob/master/Documentation/%D0%90%D0%BD%D0%B0%D0%BB%D0%B8%D0%B7%20%D1%80%D0%B8%D1%81%D0%BA%D0%BE%D0%B2.md)

# О проекте
Данное приложение предназначено для автоматизации процессов управления автопарком, водителями и рейсами.
Система позволяет эффективно распределять ресурсы, контролировать состояние транспортных средств и вести полную историю поездок.

# Основной функционал
* Управление списком транспортных средств: добавление, редактирование, удаление.

* Управление списком водителей: добавление, редактирование, удаление.

* Регистрация рейсов с привязкой водителя и транспортного средства.

* Автоматическое обновление статуса транспортных средств (например, "Доступен", "В рейсе").

* Отображение истории рейсов для водителей и транспортных средств.

* Фильтрация рейсов по дате, водителю или транспорту.

* Формирование аналитических отчётов.

# Интерфейс
* Современный дизайн (Material Design или Fluent UI).

* Поддержка светлой и тёмной тем темы оформления.

* Удобная навигация через главное меню:

  * Транспортные средства

  * Водители

  * Рейсы

  * Отчёты

# Технологии
* WPF (Windows Presentation Foundation)

* C# (.NET 6/7)

* MVVM архитектура

* SQL Server (или аналогичная СУБД)

# Установка и запуск
1. Клонируйте репозиторий:

```
git clone https://github.com/187k/FleetManagementDb.git
```
2. Откройте проект в Visual Studio.

3. Настройте строку подключения к вашей базе данных.

4. Постройте решение и запустите приложение.

# Роли пользователей
* **Администратор:** управление пользователями, системные настройки.

* **Диспетчер:** регистрация и контроль рейсов.

* **Водитель:** просмотр своих рейсов и истории поездок.

# Скриншоты

![](https://github.com/187k/VehicleControl/blob/main/img/1.png)
![](https://github.com/187k/VehicleControl/blob/main/img/2.png)
