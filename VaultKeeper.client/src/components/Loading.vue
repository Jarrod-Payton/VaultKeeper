<template>
  <div class="row">
    <div class="col-12">
      <div class="h-100vh">
        <div class="d-flex justify-content-center align-items-center">
          <i class="mdi mdi-loading mdi-spin spinner" />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  setup() {

  },
}
</script>
<style scoped>
.h-100vh {
  height: 60vh;
}
.spinner {
  font-size: 20vh;
  color: var(--bs-danger);
}
</style>