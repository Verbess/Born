# 设计思路

```
// 插槽数据基类
// 可用于技能/物品，物品和技能继承它
abstract class SlotDataBase
```

```
class SlotGo : MonoBehaviour, IComparable<Slot>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    // 属性

    // 方法
    virtual Init<T>(T slotData) where T : SlotDataBase // 初始化, 包括名字、数量、图片等
    virtual Use() // 使用物品/技能
    virtual ShowInfo() // 显示物品信息/技能
    virtual HideInfo() // 隐藏物品信息/技能
    int CompareTo() // 实现IComparable
    void OnPointerEnter() // 实现IPointerEnter, 可调用ShowInfo()
    void OnPointerExit() // 实现IPointerExit, 可调用HideInfo()
    void OnBeginDrag() // 实现IBeginDrag, 记录拖拽起始坐标差值
    void OnDrag() // 实现IDrag, 实时更新拖拽时的坐标
    void OnEndDrag() // 实现IEndDrag, 拖拽结束, 与目标SlotGo调换位置

SlotGo 附加组件:
    CanvasGroup 用于拖拽时取消Blocks Raycasts
```

```
class BagPanel : MonoBehaviour
    // 属性
    SlotGo[] slotGoArray { get; set; } // 所有插槽
    SlotGo slotInDragging { get; set; } // 当前拖拽的插槽
    SlotGo targetSlot { get; set; } // 目标插槽
    // 方法
    void OnEndDrag() // 当结束拖拽时, 使用transform.SetSiblingIndex设置两个SlotGo的层级

BagPanel 附加组件:
    GridLayoutGroup 将插槽按Grid方式自动排列, 当SlotGo的SiblingIndex变更时, SlotGo坐标会随之变化
```
