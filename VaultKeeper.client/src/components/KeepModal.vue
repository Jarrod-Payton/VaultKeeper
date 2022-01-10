<template>
  <div class="modal fade" size="modal-xl" id="keepModal">
    <div class="modal-dialog">
      <div class="modal-content"></div>
    </div>
  </div>
</template>
<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  setup() {
    return {
      activeKeep: computed(() => AppState.activeKeep)
    }
  },
}
</script>
<style scoped>
</style>