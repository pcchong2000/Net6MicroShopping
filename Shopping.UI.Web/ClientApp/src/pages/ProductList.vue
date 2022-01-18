<template>
  <el-container  class="container-body">
    <el-main>
      <el-row :gutter="20">
        <el-col v-for="item in productList" :key="item.id" :span="6" class="product-item">
          <router-link :to="'/product/'+item.id"  class="product-item-link">
            <el-image :src="item.imageUrl" class="product-item-image" fit="contain"></el-image>
            <div class="product-item-title">{{item.name}}</div>
            <div class="product-item-price">{{item.price}}</div>
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

const productList=ref([]);
productService.getProductList({pageIndex:1,pageSize:10}).then(resp=>{
  productList.value=resp.list;
})
</script>

<style scoped>
.product-item{
  display: flex;
  flex-direction: column;
  align-items: center;
  line-height: 40px;
  margin-bottom: 16px;
}
.product-item-link{
  text-decoration: none;
}
.product-item-image{
  width: 180px;
  height: 180px;
}
.product-item-title{
  color: rgb(82, 82, 82);
  line-height: 20px;
}
.product-item-price{
  color: rgb(255, 0, 0);
  line-height: 20px;
}
</style>
