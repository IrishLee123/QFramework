# QFramework框架设计理念
---
##架构分层
###表现层（View）

>数据展示层，包含UI界面和模型表现，是数据经过业务逻辑层加工后的展示窗口

实现IController接口，负责接收用户输入以及当前数据状态变化时更新UI界面展示，一般情况下MonoBehaviour均为表现层对象。

| UI组件 | MonoBehaviour | UI框架 |
| :----: | :-----------: | :----: |
---
###系统层（System）

>结合数据提供上层服务，提供各种API，实现业务逻辑

实现ISystem接口，结合底层Model层数据、使用Utility层服务，实现特定的业务逻辑，为表现层提供服务。

| 商城系统 | 背包系统 | 战斗系统 | 网络服务 |  ···  |
| :------: | :------: | :------: | :------: | :---: |
---
###数据模块层（Model）

>管理用户数据，提供数据的增删改查功能

实现IModel接口，负责数据的定义以及数据的增删改查功能实现。

| 各种Model |
| :-------: |
---
###基础工具层（Utility）

>提供基础支持，通用型较高，可复用于多个项目。

实现IUtility接口，提供基础设施，即比较通用的业务模块。

| 数据存储 | 网络连接 | 数据加密和解密 | 字符工具 | MathUtil |  ···  |
| :------: | :------: | :------------: | :------: | :------: | :---: |
---

##使用规则
* IController更改ISystem、IModel的状态必须用Command
* ISystem、IModel状态发生变更后通知IController必须用事件或BindableProeprty
* IController可以获取ISystem、IModel对象来进行数据查询
* ICommand不能有状态
* 上层可以直接获取下层对象，下层不能获取上层对象
* 下层向上层通信用时间的方式
* 上层向下层通信用方法调用，IController的交互逻辑特殊，只能使用Command


