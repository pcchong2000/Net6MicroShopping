<template>
  <el-container  class="container-body">
    <el-main>
      <div class="product-image">
        <el-image :src="productModel.imageUrl" class="product-image-class" fit="contain"></el-image>
      </div>
      <div class="product-info">
        <div class="product-info-name">{{productModel.name}}</div>
        <div class="product-info-price">{{productModel.price}}</div>
      </div>
      <div  class="product-buy">
        <el-button type="primary">加购物车</el-button>
        <el-button type="primary">立即购买</el-button>
      </div>
    </el-main>
  </el-container>
</template>

<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import productService from '../services/productService'
//获取路由参数
const route=useRoute();
//console.log(route.params.id)
const id=route.params.id;
const productModel=ref({});
productService.getProductDetail({ProductId:id}).then(resp=>{
  console.log(resp);
  productModel.value=resp;
})
</script>

<style scoped>

</style>
