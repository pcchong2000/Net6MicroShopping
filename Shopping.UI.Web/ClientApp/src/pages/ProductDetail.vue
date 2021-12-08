<template>
  <el-container  class="container-body">

    <el-main>
      {{productModel.name}}
    </el-main>
  </el-container>
</template>

<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import productService from '../services/productService'
//获取路由参数
const route=useRoute();
console.log(route.params.id)

const productModel=ref(
  {
    name:"iPhone 11",
    imageUrl:"/assets/product_default.jpeg",
    description:"",
    price:199.00,
    storeName:"深圳宝安店"
  }
);
// productService.getProductList({}).then(resp=>{
//   productList.value=resp.list;
// })
</script>

<style scoped>

</style>
