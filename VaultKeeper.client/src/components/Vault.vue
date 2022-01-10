<template>
  <div class="card elevation-2 bg-dark m-2 action">
    <div class="card-title">
      <div class="d-flex justify-content-between align-items-center">
        <div class="vault-owner text-center text-shadow pt-2 ps-3 text-light">
          {{ vault.creator.name }}
        </div>
        <button class="btn btn-sm btn-outline-danger m-1">
          <i class="mdi mdi-trash-can" />
        </button>
      </div>
    </div>
    <div class="card-body pt-0">
      <div class="col-12">
        <div class="vault-title text-shadow">
          <b>
            {{ vault.name }}
          </b>
        </div>
      </div>
      <div class="col-12">
        <div
          class="
            card
            bg-danger
            p-2
            mt-2
            isPrivate
            text-center text-light text-shadow
          "
          v-if="vault.isPrivate"
        >
          Private
        </div>
        <div
          class="
            card
            bg-warning
            p-2
            mt-2
            isPrivate
            text-center text-light text-shadow
          "
          v-else
        >
          Public
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    return {

    }
  },
}
</script>
<style scoped>
.vault-title {
  font-size: 1.8vh;
}
.vault-owner {
  font-size: 1.3vh;
}
.isPrivate {
  font-size: 1.2vh;
}
</style>