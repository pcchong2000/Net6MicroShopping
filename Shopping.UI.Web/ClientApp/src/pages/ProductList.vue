<template>
  <el-container  class="container-body">

    <el-main>
      <el-row :gutter="20">
        <el-col v-for="item in productList" :key="item.id" :span="6" class="product-item">
          <router-link :to="'/product/'+item.id">
            <el-image :src="item.imageUrl" fit="fill"></el-image>
            <div>{{item.name}}</div>
            <div>{{item.price}}</div>
          </router-link>
        </el-col>
      </el-row>
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

const productList=ref([
  {id:"1",name:"iPhone 11",imageUrl:"/images/product_default.jpeg",description:"",price:199.00,storeName:"深圳宝安店"},
  {id:"2",name:"iPhone 12",imageUrl:"/images/product_default.jpeg",description:"",price:299.00,storeName:"深圳宝安店"},
  {id:"3",name:"iPhone 13",imageUrl:"/images/product_default.jpeg",description:"",price:399.00,storeName:"深圳宝安店"},
  {id:"4",name:"iPhone 14",imageUrl:"/images/product_default.jpeg",description:"",price:499.00,storeName:"深圳宝安店"},
  {id:"5",name:"iPhone 15",imageUrl:"/images/product_default.jpeg",description:"",price:399.00,storeName:"深圳宝安店"},
  {id:"6",name:"iPhone 16",imageUrl:"/images/product_default.jpeg",description:"",price:499.00,storeName:"深圳宝安店"},
]);
// productService.getProductList({}).then(resp=>{
//   productList.value=resp.list;
// })
</script>

<style scoped>
.product-item{
  display: flex;
  flex-direction: column;
  align-items: center;
  line-height: 40px;
}



</style>
