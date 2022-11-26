<template>
  <el-container  class="container-body">
    <el-main>
      <div class="product-image">
        <el-image :src="product.imageUrl" class="product-image-class" fit="contain"></el-image>
      </div>
      <div class="product-info">
        <div class="product-info-name">{{product.name}}</div>
        <div class="product-info-price">{{product.price}}</div>
      </div>
      <div  class="product-buy">
        <el-button type="primary">加购物车</el-button>
        <el-button type="primary" @click="showShoppingView">立即购买</el-button>
      </div>
    </el-main>
    <el-dialog v-model="dialogSubmitRefVisible" :title="product.name">
      <el-form ref="submitRef" label-position="left" :model="orderItem" label-width="80px">
        <el-form-item   
        v-for="item in productModelCategory" 
        :key="item.code" 
        :label="item.name">
          <el-radio-group v-model="productModelSelect[item.code]" @change="changeProductModel($event,item.code)">
            <el-radio v-for="itemValue in item.items" :key="itemValue"  :label="itemValue" />
          </el-radio-group>
        </el-form-item>
        <el-form-item label="数量">
          <el-input-number v-model="orderItem.number" :min="1" :max="100" />
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogSubmitRefVisible = false">取消</el-button>
          <el-button type="primary" @click="shoppingOrder">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </el-container>
</template>

<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import productService from '../services/productService'
import orderService from '../services/orderService'

//获取路由参数
const route=useRoute();
//console.log(route.params.id)
const id=route.params.id;
const dialogSubmitRefVisible=ref(false);
const orderItem=ref({});
const product=ref({});
const productModelCategory=ref([]);
const productModel=ref([]);
const productModelSelect=ref({});

productService.getProductDetail({ProductId:id}).then(resp=>{
  console.log(resp);
  product.value=resp.data;
  resp.data.storeProductModelCategoryList.forEach(item=>{
    item.items=JSON.parse(item.items);
    productModelSelect.value[item.code]='';
  });
  console.log(resp.storeProductModelCategoryList);
  productModelCategory.value=resp.data.storeProductModelCategoryList;

  resp.data.storeProductModelList.forEach(item=>{
    item.value=JSON.parse(item.value);
  });
  productModel.value=resp.data.storeProductModelList;
});

function init(){
  
}
function changeProductModel(e,code){
  let productModelSelectProCount=0;
  for(let i in productModelSelect.value){
    if(productModelSelect.value[i]){
      productModelSelectProCount++;
    }
    
  }
  if(productModelSelectProCount==productModelCategory.value.length){
      let productModelList=[];
      for(let i in productModelSelect.value){
        productModelList = productModel.value.filter(a=>a.value[i]==productModelSelect.value[i]);
      }
      console.log(productModelList);
      if(productModelList.length>0){
        orderItem.value.productModelId=productModelList[0].id;
      }
  }
}
function showShoppingView(){
  dialogSubmitRefVisible.value=true;
}
function shoppingOrder(){
  const submitModel={
    storeId:product.value.storeId,
    storeName:product.value.storeName,
    orderItems:[{
      productId:product.value.id,
      number:orderItem.value.number,
      productModelId:orderItem.value.productModelId,
    }]
  };
  orderService.post(submitModel).then(resp=>{
    console.log(resp);
    //dialogSubmitRefVisible.value=false;
  });
}

</script>

<style scoped>

</style>
