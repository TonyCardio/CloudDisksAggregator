﻿# Cloud Disks Aggregator

## А кто писал?
```
Шумилин Илья КН-204
Левагин Данил КН-204
Нечуговских Антон КН-204
```

## А зачем?
При использовании нескольких облачных хранилищ возникает проблема менеджмента своих данных. Необходимо помнить какие данные в каких облаках хранятся, и зачастую в самом облаке возникает хаос.

Решение этой проблемы это агрегация и синхронизация хранилищ пользователя - предоставление единого интерфейса для взаимодействия.

## А как там с архитектурой?

Проект разбит на сборки (слои) согласно DDD

* `CloudDisksAggregatorUI` - само ядро приложения, зависящее от остальных сборок. Тут же собираем всё DI-контейнером.
* `CloudDisksAggregator` - место для реализаций `ICloudApi` и используемых ими классов предметной области.
* `CloudDisksAggregatorInfrastructure` - инфраструктура
* `CloudDisksAggregatorTests` - тесты

## А что с функционалом?

Можно глянуть [здесь](Features.md)

## А что и как расширять?

### `Облачные диски`
Диски подключаются подобно плагинам, поэтому достаточно создать новую реализацию интерфейса `CloudDisksAggregator.Core.ICloudApi`


### `Отображение файлов`
Отнаследуйтесь от класса `CloudDisksAggregatorUI.FileContent.FileViewers.FileViewer` и вот уже на ваши видео/архивы/файлы дампов и прочее можно посмотреть прямо в приложении