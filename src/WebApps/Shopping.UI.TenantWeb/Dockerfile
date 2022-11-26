FROM node as build-stage
WORKDIR /app
RUN npm config set registry https://registry.npm.taobao.org
COPY . .
RUN npm install && npm run build


FROM nginx:alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
