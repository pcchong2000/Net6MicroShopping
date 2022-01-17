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
              <el-upload
                class="avatar-uploader"
                :action="uploadUrl"
                :show-file-list="false"
                :on-success="handleImageSuccess"
                
              >
                <el-image v-if="submitData.imageUrl" :src="submitData.imageUrl" fit="contain" class="avatar" />
                <el-icon v-else class="avatar-uploader-icon"><plus /></el-icon>
              </el-upload>

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
      </el-tab-pane>
      <el-tab-pane label="商品规格">
        
      </el-tab-pane>
    </el-tabs>
    <el-form ref="submitbtnref" label-position="left" label-width="120px">
      <el-form-item>
        <el-button  @click="cancel">取消</el-button>
        <el-button type="primary" @click="submit">确定</el-button>
      </el-form-item>
    </el-form>
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
      productId:"",
      uploadUrl:"",
    };
  },
  created(){
    this.productId=this.$route.query.id;
    this.storeId=localStorage.getItem("storeId");
    this.storeName=localStorage.getItem("storeName");
    this.uploadUrl=apiurl+"/file";
    this.getDetail();
    this.getProductCategory();
    this.getStoreProductCategory();
  },
  methods:{
    getDetail(){
      let query={productId:this.productId};
      productService.getProductDetail(query).then(a=>{
        console.log(a);
        this.submitData=a;
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
          //this.getDataList();
        });

      }else{
        productService.postProduct(this.submitData).then(resp=>{
          this.dialogVisible=false;
          //this.getDataList();
        });
      }
      
    },
    cancel(){
      this.$router.go(-1);
    },
    handleImageSuccess(resp,file){
      console.log(resp);
      this.submitData.imageUrl=apiurl+resp[0].pathUrl;
    }
  }
}
</script>

<style scoped>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
