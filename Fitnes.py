users = {
    'login': '1',
    'password': '123',
    'role': 'user',
    'subscription_type': 'Premium',
     'created_at': '2024-12-12'}

admins = {
    'login': '2',
    'password': '456',
    'role': 'admin'}

history=[]

cart=[]

cena=0

services = {
    'Абонемент(год)': 30000,
    'Массаж': 4000,
    'Пользование сауной/баней': 10000,
    'Вводная тренировка(1 сеанс)': 2000,
    'Персональный тренер(1 сеанс)': 3000,
    'Занятие йогой(совместная)': 1000}

while True:
    print("Здравствуйте! Как кто вы хотите войти?")
    print("1. Как пользователь")
    print("2. Как админ")
    try:
        ver=int(input())
    except ValueError:
        print("Ошибка выбора! Введите цифру.")

    if ver == 1:
        print("Добро пожаловать в фитнес-клуб  Iron Will Gym!")
        print("Пожалуйста авторизуйтесь")

        while ver == '1':
            print("Логин: ")
            log = input()
            print("Пароль: ")
            pas = input()

            if log == users.get('login') and pas ==  users.get('password'):
                print ("Авторизация прошла успешно!")
                break
            else:
                print ("Неверный логин или пароль, попробуйте ещё раз!")
        v=0 

        while v!=6:
            print (f"\nВыберите действие: ")
            print ("1. Просмотреть каталога услуг")
            print ("2. Найти услугу")
            print ("3. Сортировать услуги по цене")
            print ("4. Простмотр корзины")
            print ("5. Просмотр истории покупок")
            print ("6. Изменение пароля")
            print ("7. Выйти")

            v=input()
            i=0
            n=0

            if v=='1':
                for key, value in services.items():
                    i=i+1
                    print(i, key, value)
                while True:
                    print("Введите цену услуги для добавления ее в корзину или 0 для выхода из каталога")
                    try:
                        cell=int(input())
                    except ValueError:
                        print("Введите цену услуги или 0 для выхода")
                        continue
                    if cell==0:
                        break
                    else:
                        key = next((key for key, value in services.items() if value == cell), None)
                        cart.append(key)
                        cart.append(f"{cell} руб.")
                        print ("Введите количество:")
                        try:
                            kol=int(input())
                        except ValueError:
                            print("Ошибка! Введите количество")
                            continue
                        cart.append(f"{kol} Штук")
                        while kol !=0:
                            cena= cena + cell
                            kol=kol-1
                        
                        continue

            if v=='2':
                print(f"\nВведите название услуги:")
                get=input()
                get1  = services.get(get)

                if get1 !=None:
                    print(f"Услуга найдена\nЦена услуги: {get1}")
                    print("Хотите добавить в корзину?")
                    print("1. Да")
                    print("2. Нет")
                    vari=input()

                    if vari == '1':
                        print("Какое количество?")
                        try:
                            kol = int(input())
                        except ValueError:
                            print("Пожалуйста, введите число.")
                            continue
                        cart.append(get)
                        cart.append(f"{get1} руб.")
                        cart.append(f"{kol} Штук")
                        while kol !=0:
                           cena= cena + get1
                           kol=kol-1
                        print("Товар добавлен в корзину")
                        continue  
                        
                    if vari == '2':
                        continue

                else:
                     print("Ошибка! Такой услуги нет в каталоге")
                     continue
                     
            if v=='3':
                print("Отсортированный список: ")
                services2 = sorted(services.items(), key=lambda x: x[1])
                sortservices = dict(services2)
                for key, value in sortservices.items():
                    i=i+1
                    print(i, key, value)
                while True:
                    print("Введите цену услуги для добавления ее в корзину или 0 для выхода из каталога")
                    try:
                        cell=int(input())
                    except ValueError:
                        print("Введите цену услуги или 0 для выхода")
                        continue
                    if cell==0:
                        break
                    else:
                        key = next((key for key, value in services.items() if value == cell), None)
                        cart.append(key)
                        cart.append(f"{cell} руб.")
                        print ("Введите количество:")
                        try:
                            kol=int(input())
                        except ValueError:
                            print("Ошибка! Введите количество")
                            continue
                        cart.append(f"{kol} Шт.")
                        while kol !=0:
                            cena= cena + cell
                            kol=kol-1
                        continue

            if v=='4':
                cart.append(f"Итоговая цена: {cena} руб.")
                print(cart)
                print("Хотите оформить заказ?")
                print("1. Да")
                print("2. Нет")
                try:
                    vib=int(input())
                except ValueError:
                    print("Ошибка! Введите 1 или 2")
                if vib==1:
                    n=n+1
                    history.append(f"Заказ {n}")
                    history.append(cart)
                    cart.clear
                    print("Заказ оформлен! Спасибо за покупку!")
                if vib==2:
                    cart.pop()
                continue

            if v=='5':
                print("История покупок")
                print(history)

            if v=='6':
                print("Введите новвый пароль:")
                newpassword=input()
                users['password'] = newpassword
                print(users)
                continue

            if v=='7':
                break

            else:
                print("Ошибка выбора! Введите цифру.")
                continue                   

    if ver == 2:
        print("Добро пожаловать в управление фитнес-клубом Iron Will Gym!")
        print("Пожалуйста авторизуйтесь")

        while ver == 2:
            print("Логин: ")
            log = input()
            print("Пароль: ")
            pas = input()
            print(admins)

            if log == admins.get('login') and pas ==  admins.get('password'):
                print ("Авторизация прошла успешно!")
                break
            else:
                print ("Неверный логин или пароль, попробуйте ещё раз!")
        n=0

        while n!=5:
            print ("Выберите действие: ")
            print ("1. Добавить услугу")
            print ("2. Удалить услугу")
            print ("3. Редактировать данные о услуге")
            print ("4. Управление пользователями")
            print ("5. Выйти")
            try:
                n=int(input())
            except ValueError:
                print("Ошибка выбора! Введите цифру.")
            if n == 1:
                print("Введите название услуги:")
                try:
                    service=input()
                except ValueError:
                    ("Ошибка ввода услуги")
                    break
                print("Введите цену услуги:")
                try:
                    price=input()
                except ValueError:
                    ("Ошибка ввода цены")                
                services[service]=price
                print(services)
                continue
            if n == 2:
                print("Введите название услуги:")
                try:
                    service=input()
                except ValueError:
                    ("Ошибка ввода услуги")
                    continue
                try:
                    services.pop(service)
                except KeyError:
                    ("Такой услуги нет")
                    continue
                print("Услуга удалена") 
                print(services)
                continue
            if n == 3: 
                print("Выбирите какие данные редактировать")
                print("1. Название услуги")
                print("2. Цену услуги")
                try:
                    ver=int(input())
                except ValueError:
                    print("Ошибка выбора! Введите цифру.") 
                if ver == 1:
                    print("Введите название услуги:")
                    cervice=input()
                    print("Введите новое название услуги:")
                    cervice2=input()
                    price = services[cervice]
                    services[cervice2]= price
                    services.pop(cervice)
                    print(services)
                    continue
                if ver == 2:
                    print("Введите название услуги:")
                    try:
                        kol = int(input())
                    except ValueError:
                        print("Пожалуйста, введите число.")
                    print("Введите новое цену услуги:")
                    price=int(input())
                    services[cervice]= price
                    print(services)
                    continue    
            if n == 4: 
                break    

    else:
        print("Ошибка выбора! Введите цифру.")
        continue
