using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemicon : UIDragDropItem
{
    private int skillid;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        this.skillid = transform.parent.GetComponent<SkillItem>().id;
      
        transform.parent = transform.root;
        this.GetComponent<UISprite>().depth = 31;
    }
  
    
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if(surface!=null&&surface.tag=="shortcut")
        {
            
            surface.GetComponent<ShortcutGrid>().SetSkill(this.skillid);
           
        }
    }
}
