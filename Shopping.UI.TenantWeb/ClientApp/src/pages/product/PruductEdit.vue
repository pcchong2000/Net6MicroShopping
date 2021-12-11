<template>
  <div>
    <el-tabs type="card" @tab-click="handleClick">
      <el-tab-pane label="基本信息">
        <el-form ref="submitref" label-position="left" :model="submitData" label-width="120px">
          <el-form-item label="商品全站分类">
            <el-select v-model="submitData.productCategoryId" >
              <el-option
                v-for="item in productCategoryList"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
            </el-option>
          </el-select>
          </el-form-item>
          <el-form-item label="商品门店分类">
            <el-select v-model="submitData.storeProductCategoryId" >
              <el-option
                v-for="item in storeProductCategoryList"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
            </el-option>
          </el-select>
          </el-form-item>
          <el-form-item label="商品名称">
            <el-input v-model="submitData.name"></el-input>
          </el-form-item>
          <el-form-item label="编号">
            <el-input v-model="submitData.code"></el-input>
          </el-form-item>
          <el-form-item label="图片">
            <el-input v-model="submitData.imageUrl"></el-input>
          </el-form-item>
          <el-form-item label="价格">
            <el-input v-model="submitData.price"></el-input>
          </el-form-item>
          <el-form-item label="排序">
            <el-input v-model="submitData.sort"></el-input>
          </el-form-item>
          <el-form-item label="描述">
            <el-input v-model="submitData.description"></el-input>
          </el-form-item>
          <el-form-item label="配置规格">
            <el-input v-model="submitData.description"></el-input>
          </el-form-item>
        </el-form>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogVisible = false">取消</el-button>
            <el-button type="primary" @click="submit">确定</el-button>
          </span>
        </template>
      </el-tab-pane>
      <el-tab-pane label="商品规格">
        
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script >
import  productService  from '../../services/productService'
export default {
  name: 'UserList',

  data:()=>{
    return {
      submitData:{},
      productCategoryList:[],
      storeProductCategoryList:[],
      storeId:"",
      storeName:"",
    };
  },
  created(){
    this.storeId=localStorage.getItem("storeId");
    this.storeName=localStorage.getItem("storeName");
    this.getDetail();
    this.getProductCategory();
    this.getStoreProductCategory();
  },
  methods:{
    getDetail(){
      productService.getProductList(this.searchData).then(a=>{
        console.log(a);
        this.dataList=a.list;
      });
    },
    getProductCategory(){
      productService.getProductCategoryList({pageIndex:1,pageSize:20}).then(a=>{
        console.log(a);
        this.productCategoryList=a.list;
      });
    },
    getStoreProductCategory(){
      productService.getStoreProductCategoryList({pageIndex:1,pageSize:20,storeId:this.storeId}).then(a=>{
        console.log(a);
        this.storeProductCategoryList=a.list;
      });
    },
    submit(){

      this.submitData.storeId=this.storeId;
      this.submitData.storeName=this.storeName;
      this.submitData.status=1;
      this.submitData.storeProductModelCategoryList=[];
      this.submitData.storeProductModelList=[];
      if(this.submitData.id){
        productService.putProduct(this.submitData).then(resp=>{
          this.dialogVisible=false;
          this.getDataList();
        });

      }else{
        productService.postProduct(this.submitData).then(resp=>{
          this.dialogVisible=false;
          this.getDataList();
        });
      }
      
    },
  }
}
</script>

<style scoped>

</style>
