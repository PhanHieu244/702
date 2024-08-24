using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
   public void OnPressDown(int i)
   {
      switch (i)
      {
         case 1:
            GameManager.instance.AddCoin(1);
            GameDataManager.Instance.playerData.AddDiamond(1);
             IAPManager.Instance.BuyProductID(IAPKey.PACK1);
            break;
         case 2:
            GameManager.instance.AddCoin(3);
            GameDataManager.Instance.playerData.AddDiamond(3);
            IAPManager.Instance.BuyProductID(IAPKey.PACK2);
            break;
         case 3:
            GameManager.instance.AddCoin(5);
            GameDataManager.Instance.playerData.AddDiamond(5);
            IAPManager.Instance.BuyProductID(IAPKey.PACK3);
            break;
         case 4:
            GameManager.instance.AddCoin(10);
            GameDataManager.Instance.playerData.AddDiamond(10);
            IAPManager.Instance.BuyProductID(IAPKey.PACK4);
            break;
      }
   }

   public void Sub(int i)
   {
      GameDataManager.Instance.playerData.SubDiamond(i);
   }
}
